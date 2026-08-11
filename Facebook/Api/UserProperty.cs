using System;

namespace Facebook.Api
{
    [Flags]
    public enum UserProperty : long
    {
        [FieldName("uid")]
        Uid                         = 0x1,

        [FieldName("about_me")]
        AboutMe                     = 0x2,

        [FieldName("activities")]
        Activities                  = 0x4,

        [FieldName("affiliations")]
        Affiliations                = 0x8,

        [FieldName("birthday")]
        Birthday                    = 0x10,

        [FieldName("birthday_date")]
        BirthdayDate                = 0x20,

        [FieldName("books")]
        Books                       = 0x40,

        [FieldName("current_location")]
        CurrentLocation             = 0x80,

        [FieldName("education_history")]
        EducationHistory            = 0x100,

        [FieldName("email_hashes")]
        EmailHashes                 = 0x200,

        [FieldName("first_name")]
        FirstName                   = 0x400,

        [FieldName("hometown_location")]
        HometownLocation            = 0x800,

        [FieldName("hs_info")]
        HighSchoolInfo              = 0x1000,

        [FieldName("interests")]
        Interests                   = 0x2000,

        [FieldName("is_app_user")]
        IsAppUser                   = 0x4000,

        [FieldName("is_blocked")]
        IsBlocked                   = 0x8000,

        [FieldName("last_name")]
        LastName                    = 0x10000,

        [FieldName("locale")]
        Locale                      = 0x20000,

        [FieldName("meeting_for")]
        MeetingFor                  = 0x40000,

        [FieldName("meeting_sex")]
        MeetingSex                  = 0x80000,

        [FieldName("movies")]
        Movies                      = 0x100000,

        [FieldName("music")]
        Music                        = 0x200000,

        [FieldName("notes_count")]
        NotesCount                  = 0x400000,

        [FieldName("pic")]
        Pic                         = 0x800000,

        [FieldName("pic_with_logo")]
        PicWithLogo                 = 0x1000000,

        [FieldName("pic_big")]
        PicBig                      = 0x2000000,

        [FieldName("pic_big_with_logo")]
        PicBigWithLogo              = 0x4000000,

        [FieldName("pic_small")]
        PicSmall                    = 0x8000000,

        [FieldName("pic_small_with_logo")]
        PicSmallWithLogo            = 0x10000000,

        [FieldName("pic_square")]
        PicSquare                   = 0x20000000,

        [FieldName("pic_square_with_logo")]
        PicSquareWithLogo           = 0x40000000,

        [FieldName("political")]
        Political                   = 0x80000000,

        [FieldName("profile_blurb")]
        ProfileBlurb                = 0x100000000,

        [FieldName("profile_update_time")]
        ProfileUpdateTime           = 0x200000000,

        [FieldName("profile_url")]
        ProfileUrl                  = 0x400000000,

        [FieldName("proxied_email")]
        ProxiedEmail                = 0x800000000,

        [FieldName("quotes")]
        Quotes                      = 0x1000000000,

        [FieldName("relationship_status")]
        RelationshipStatus          = 0x2000000000,

        [FieldName("religion")]
        Religion                    = 0x4000000000,

        [FieldName("sex")]
        Sex                         = 0x8000000000,

        [FieldName("significant_other_id")]
        SignificantOtherId          = 0x10000000000,

        [FieldName("status")]
        Status                      = 0x20000000000,

        [FieldName("timezone")]
        TimeZone                    = 0x40000000000,

        [FieldName("tv")]
        TV                          = 0x80000000000,

        [FieldName("username")]
        Username                    = 0x100000000000,

        [FieldName("wall_count")]
        WallCount                   = 0x200000000000,

        [FieldName("work_history")]
        WorkHistory                 = 0x400000000000,

        [FieldName("name")]
        Name                        = 0x800000000000,

        [FieldName("anniversary")]
        Anniversary                 = 0x1000000000000,

        [FieldName("anniversary_date")]
        AnniversaryDate             = 0x2000000000000
    }
}