using System;

namespace Facebook
{
    [Flags]
    public enum ExtendedPermission : long
    {
        [Permission]
        [FieldName("publish_stream")]
        PublishStream               = 0x1,

        [Permission]
        [FieldName("read_stream")]
        ReadStream                  = 0x2,

        [Permission]
        [FieldName("email")]
        Email                       = 0x4,

        [Permission]
        [FieldName("read_mailbox")]
        ReadMailbox                 = 0x8,

        [Permission]
        [FieldName("offline_access")]
        OfflineAccess               = 0x10,

        [Permission]
        [FieldName("create_event")]
        CreateEvent                 = 0x20,

        [Permission]
        [FieldName("sms")]
        Sms                         = 0x40,

        [Permission]
        [FieldName("status_update")]
        StatusUpdate                = 0x80,

        [Permission]
        [FieldName("photo_upload")]
        PhotoUpload                 = 0x100,

        [Permission]
        [FieldName("video_upload")]
        VideoUpload                 = 0x200,

        [Permission]
        [FieldName("create_note")]
        CreateNote                  = 0x400,

        [Permission]
        [FieldName("share_item")]
        ShareItem                   = 0x800,

        [Permission(CanBeRequested = false)]
        [FieldName("auto_publish_recent_activity")]
        AutoPublishRecentActivity   = 0x1000,

        [Permission(CanBeRequested = false)]
        [FieldName("bookmarked")]
        Bookmarked                  = 0x2000,

        [Permission(CanBeRequested = false)]
        [FieldName("contact_email")]
        ContactEmail                = 0x4000,

        [Permission(CanBeRequested = false)]
        [FieldName("auto_publish_short_feed")]
        AutoPublishShortFeed        = 0x8000
    }
}