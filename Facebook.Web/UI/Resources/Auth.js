jQuery(function() {
    jQuery('input[type=button]#authorize').click(function() { Auth.init(); jQuery('div#warning').slideUp('fast'); });
    jQuery('input[type=button]#noauth').click(function() {
        Auth.redirect('http://facebook.com');
    });
});

function Auth() { }

Auth.data = {
    isAuthorized: false,
    requestedRequiredPerms: [],
    requestedOptionalPerms: [],
    acquiredRequiredPerms: [],
    acquiredOptionalPerms: [],
    missingRequiredPerms: [],
    missingOptionalPerms: [],
    strRequiredPerms: [],
    strOptionalPerms: [],
    strRequestedPerms: '',
    apiKey: null,
    receiverUrl: null,
    returnUrl: null,
    strRequestedRequired: null,
    strRequestedOptional: null,
    duration: null
};

Auth.init = function(apiKey, receiverUrl, returnUrl, strRequestedRequired, strRequestedOptional, duration) {
    jQuery('div.loadSession').slideDown('fast');

    Auth.data = {
        isAuthorized: false,
        requestedRequiredPerms: strRequestedRequired ? strRequestedRequired.split(',') : Auth.data.requestedRequiredPerms,
        requestedOptionalPerms: strRequestedOptional ? strRequestedOptional.split(',') : Auth.data.requestedOptionalPerms,
        acquiredRequiredPerms: [],
        acquiredOptionalPerms: [],
        missingRequiredPerms: [],
        missingOptionalPerms: [],
        strRequiredPerms: [],
        strOptionalPerms: [],
        strRequestedPerms: '',
        apiKey: apiKey || Auth.data.apiKey,
        receiverUrl: receiverUrl || Auth.data.receiverUrl,
        returnUrl: returnUrl || Auth.data.returnUrl,
        strRequestedRequired: strRequestedRequired || Auth.data.strRequestedRequired,
        strRequestedOptional: strRequestedOptional || Auth.data.strRequestedOptional,
        duration: duration || Auth.data.duration
    };

    FB_RequireFeatures(['Connect'], function() {
        FB.Facebook.init(apiKey, receiverUrl);
        Auth.requireSession();
    });
};

Auth.requireSession = function() {
    FB.Connect.requireSession(Auth.requireSessionCallback, Auth.cancelLoginCallback);
};

Auth.requireSessionCallback = function() {    //$log('Auth.requireSessionCallback');
    Auth.data.isAuthorized = true;
    if ((Auth.data.requestedRequiredPerms.length && Auth.data.requestedRequiredPerms[0].length) || (Auth.data.requestedOptionalPerms.length && Auth.data.requestedOptionalPerms[0].length)) {
        Auth.requestPermissions();
    }
    else Auth.redirect();
};

Auth.requestPermissions = function() {
    var requiredPermQueries = [];
    var optionalPermQueries = [];
    var sequencer = new FB.BatchSequencer();
    // check to see if the user has any of the required permissions
    for (var i = 0; i < Auth.data.requestedRequiredPerms.length; i++) {
        var result = FB.Facebook.apiClient.users_hasAppPermission(Auth.data.requestedRequiredPerms[i], sequencer);
        result.perm = Auth.data.requestedRequiredPerms[i];
        requiredPermQueries.push(result);
    }
    // check to see if the user has any of the optional permissions
    for (var i = 0; i < Auth.data.requestedOptionalPerms.length; i++) {
        var result = FB.Facebook.apiClient.users_hasAppPermission(Auth.data.requestedOptionalPerms[i], sequencer);
        result.perm = Auth.data.requestedOptionalPerms[i];
        optionalPermQueries.push(result);
    }
    sequencer.execute(function() {
        for (var i = 0; i < requiredPermQueries.length; i++) {
            var query = requiredPermQueries[i];
            if (query.result) Auth.data.acquiredRequiredPerms.push(query.perm);
            else Auth.data.missingRequiredPerms.push(query.perm);
        }
        for (var i = 0; i < optionalPermQueries.length; i++) {
            var query = optionalPermQueries[i];
            if (query.result) Auth.data.acquiredOptionalPerms.push(query.perm);
            else Auth.data.missingOptionalPerms.push(query.perm);
        }

        for (var i = 0; i < Auth.data.missingRequiredPerms.length; i++) {
            if (Auth.data.strRequestedPerms) Auth.data.strRequestedPerms += ',';
            Auth.data.strRequestedPerms += Auth.data.missingRequiredPerms[i];
        };

        for (var i = 0; i < Auth.data.missingOptionalPerms.length; i++) {
            if (Auth.data.strRequestedPerms) Auth.data.strRequestedPerms += ',';
            Auth.data.strRequestedPerms += Auth.data.missingOptionalPerms[i];
        };

        jQuery('div.loadSession').slideUp('fast');

        if (Auth.data.missingRequiredPerms.length || Auth.data.missingOptionalPerms.length) FB.Connect.showPermissionDialog(Auth.data.strRequestedPerms, Auth.requirePermissionsCallback);
        else Auth.handlePermissionsResult();
    });
};

Auth.requirePermissionsCallback = function(strGranted) {
    var granted = strGranted.split(',');
    for (var i = 0; i < granted.length; i++) {
        var grantedValue = granted[i];
        if (jQuery.inArray(Auth.data.requestedRequiredPerms, grantedValue) >= 0) Auth.data.acquiredRequiredPerms.push(grantedValue);
        else if (jQuery.inArray(Auth.data.requestedOptionalPerms, grantedValue) >= 0) Auth.data.acquiredOptionalPerms.push(grantedValue);
    }
    Auth.handlePermissionsResult();
};

Auth.handlePermissionsResult = function() {
    for (var i = 0; i < Auth.data.acquiredRequiredPerms.length; i++) {
        var perm = Auth.data.acquiredRequiredPerms[i];
        var reqIndex = jQuery.inArray(perm, Auth.data.missingRequiredPerms);
        if (reqIndex >= 0) {
            Auth.data.requestedRequiredPerms.splice(reqIndex, 1);
        }
    }

    for (var i = 0; i < Auth.data.acquiredOptionalPerms.length; i++) {
        var perm = Auth.data.acquiredOptionalPerms[i];
        var reqIndex = jQuery.inArray(perm, Auth.data.missingOptionalPerms);
        if (reqIndex >= 0) {
            Auth.data.requestedOptionalPerms.splice(reqIndex, 1);
        }
    }

    if (Auth.data.missingRequiredPerms.length || Auth.data.missingOptionalPerms.length) {
        Auth.showRequiredWarning('permissions');
    }
    else Auth.redirect();
};

Auth.showRequiredWarning = function(reason) {
    var data = { isAuthorized: Auth.data.isAuthorized, required: Auth.data.missingRequiredPerms.join(','), optional: Auth.data.missingOptionalPerms.join(','), duration: Auth.data.duration }
    jQuery('div#warning div.content').load(Environment.getUrl('Auth', 'ExtendedPermissionWarning'), data, function() { jQuery('div#warning').slideDown('fast'); });
}

Auth.cancelLoginCallback = function() {
    Auth.showRequiredWarning('authorization');
};

Auth.redirect = function(cancelUrl) {
    var acquiredPerms = [];
    acquiredPerms.push.apply(acquiredPerms, Auth.data.acquiredRequiredPerms);
    acquiredPerms.push.apply(acquiredPerms, Auth.data.acquiredOptionalPerms);
    var data = {
        fb_sig_ext_perms: acquiredPerms.join(','),
        declinedOptional: Auth.data.missingOptionalPerms.join(','),
        duration: Auth.data.duration
    };
    jQuery.post(Environment.getUrl('Auth', 'SetExtendedPermissions'), data, function() {
        if (cancelUrl && Auth.data.missingRequiredPerms.length && Auth.data.missingRequiredPerms[0].length) window.location.href = cancelUrl;
        else window.location.href = Auth.data.returnUrl;
    });
};