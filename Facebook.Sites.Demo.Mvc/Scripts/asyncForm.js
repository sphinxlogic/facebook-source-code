jQuery.prototype.asyncForm = function(type, callback) {
    this.bind('submit',function(e) {
        var form = jQuery(e.target);
        var action = form.attr('action');        
        var params = form.find(':input').serializeArray();
        var result = jQuery.post(action,params,null,type);
        if(callback) callback(result);
        return false;
        });
    return this;
}