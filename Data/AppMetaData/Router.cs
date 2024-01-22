namespace Data.AppMetaData;

public static class Router
{
    #region Const Params
    public const string root = "Api";
    public const string version = "v1";
    public const string Rule = root + "/" + version + "/";
    public const string SignleRoute = "{id}";
    #endregion

    #region Student
    public static class Student
    {
        public const string Prefix = Rule + "Student/";

        public const string List = Prefix + "List";
        public const string Pagination = Prefix + "Pagination";
        public const string GetById = Prefix + "GetByID" + "/" + SignleRoute;
        public const string Create = Prefix + "Create";
        public const string Edit = Prefix + "Edit";
        public const string Delete = Prefix + "Delete" + "/" + SignleRoute;

    }

    public static class Department
    {
        public const string Prefix = Rule + "Department/";

        public const string List = Prefix + "List";
        public const string Pagination = Prefix + "Pagination";
        public const string GetById = Prefix + "GetByID";
        public const string Create = Prefix + "Create";
        public const string Edit = Prefix + "Edit";
        public const string Delete = Prefix + "Delete" + "/" + SignleRoute;

    }
    #endregion

    #region User
    public static class User
    {
        public const string Prefix = Rule + "User";
        public const string Create = Prefix + "/Create";
        public const string Paginated = Prefix + "/Paginated";
        public const string GetByID = Prefix + "/" + SignleRoute;
        public const string Edit = Prefix + "/Edit";
        public const string Delete = Prefix + "/{id}";
        public const string ChangePassword = Prefix + "/Change-Password";
    }
    #endregion

    #region Authentication
    public static class Authentication
    {
        public const string Prefix = Rule + "Authentication";
        public const string SignIn = Prefix + "/SignIn";
        public const string RefreshToken = Prefix + "/Refresh-Token";
        public const string ValidateToken = Prefix + "/Validate-Token";
        public const string ConfirmEmail = "/Api/Authentication/ConfirmEmail";
        public const string SendResetPasswordCode = Prefix + "/SendResetPasswordCode";
        public const string ConfirmResetPasswordCode = Prefix + "/ConfirmResetPasswordCode";
        public const string ResetPassword = Prefix + "/ResetPassword";

    } 
    #endregion

}
