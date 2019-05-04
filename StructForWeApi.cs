class StructForWeApi
{
	public struct UserDetail {
            public long level;
            public long listenSongs;
            public UserPoint userpoint;
            public bool mobileSign;
            public bool pcSign;
            public Profile profile;
            public bool peopleCanSeeMyPlayRecord;
            public Binding[] bindings;
            public bool adValid;
            public long code;
            public long createTime;
            public long createDays;
        }

        public struct UserPoint {
            public long userId;
            public long balance;
            public long updateTime;
            public long version;
            public long status;
            public long blockBalance;
        }

        public struct Profile {
            public string detailDescription;
            public bool followed;
            public long userId;
            public long vipType;
            public long gender;
            public long accountStatus;
            public long avatarImgId;
            public string nickname;
            public long birthday;
            public long city;
            public long province;
            public bool defaultAvatar;
            public string avatarUrl;
            public long djStatus;
            public object experts;
            public long backgroundImgId;
            public long userType;
            public bool mutual;
            public string remarkName;
            public string expertTags;
            public long authStatus;
            public string backgroundUrl;
            public string description;
            public string avatarImgIdStr;
            public string backgroundImgIdStr;
            public string signature;
            public long authority;
            public string avatarImgId_str;
            public object[] artistIdentity;
            public long followeds;
            public long follows;
            public long cCount;
            public bool blacklist;
            public long eventCount;
            public long sDJPCount;
            public long allSubscribedCount;
            public long playlistCount;
            public long playlistBeSubscribedCount;
            public long sCount;
        }

        public struct Binding {
            public long refreshTime;
            public long userId;
            public string tokenJsonStr;
            public string url;
            public long bindingTime;
            public long expiresIn;
            public bool expired;
            public long id;
            public long type;
        }

}