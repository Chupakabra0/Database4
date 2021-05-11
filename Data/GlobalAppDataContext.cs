namespace Database4.Data {
    public class GlobalAppDataContext {
        private static AppDataContext instance_;

        public static AppDataContext Instance =>
            GlobalAppDataContext.instance_ ??= new AppDataContext();
    }
}
