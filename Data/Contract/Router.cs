namespace Data.Contract;

public static class Router
{
    #region Const Params
    private const string Root = "Api";
    private const string Version = "v1";
    private const string Rule = Root + "/" + Version + "/";
    private const string SingleRoute = "{id}";
    #endregion

    #region Student
    public static class StudentRoute
    {
        private const string Prefix = Rule + "Student/";

        public const string List = Prefix + "List";
        public const string Pagination = Prefix + "Pagination";
        public const string GetById = Prefix + "GetByID" + "/" + SingleRoute;
        public const string Create = Prefix + "Create";
        public const string Edit = Prefix + "Edit";
        public const string Delete = Prefix + "Delete" + "/" + SingleRoute;

    }

    public static class Department
    {
        private const string Prefix = Rule + "Department/";

        public const string List = Prefix + "List";
        public const string Pagination = Prefix + "Pagination";
        public const string GetById = Prefix + "GetByID";
        public const string Create = Prefix + "Create";
        public const string Edit = Prefix + "Edit";
        public const string Delete = Prefix + "Delete" + "/" + SingleRoute;

    }
    #endregion

    #region User
    public static class User
    {
        private const string Prefix = Rule + "User";
        public const string Create = Prefix + "/Create";
        public const string Paginated = Prefix + "/Paginated";
        public const string GetById = Prefix + "/" + SingleRoute;
        public const string Edit = Prefix + "/Edit";
        public const string Delete = Prefix + "/{id}";
        public const string ChangePassword = Prefix + "/Change-Password";
    }
    #endregion

    #region Authentication
    public static class Authentication
    {
        private const string Prefix = Rule + "Authentication";
        public const string SignIn = Prefix + "/SignIn";
        public const string RefreshToken = Prefix + "/Refresh-Token";
        public const string ValidateToken = Prefix + "/Validate-Token";
        public const string ConfirmEmail = "/Api/Authentication/ConfirmEmail";
        public const string SendResetPasswordCode = Prefix + "/SendResetPasswordCode";
        public const string ConfirmResetPasswordCode = Prefix + "/ConfirmResetPasswordCode";
        public const string ResetPassword = Prefix + "/ResetPassword";

    }
    #endregion

    #region Authorization
    public static class Authorization
    {
        private const string Prefix = Rule + "Authorization";
        private const string Roles = Prefix + "/Roles";
        private const string Claims = Prefix + "/Claims";
        public const string Create = Roles + "/Create";
        public const string Edit = Roles + "/Edit";
        public const string Delete = Roles + "/Delete/{id}";
        public const string RoleList = Roles + "/Role-List";
        public const string GetRoleById = Roles + "/Role-By-Id/{id}";
        public const string ManageUserRoles = Roles + "/Manage-User-Roles/{userId}";
        public const string ManageUserClaims = Claims + "/Manage-User-Claims/{userId}";
        public const string UpdateUserRoles = Roles + "/Update-User-Roles";
        public const string UpdateUserClaims = Claims + "/Update-User-Claims";
    }


    #endregion

    #region Emails
    public static class Emails
    {
        private const string Prefix = Rule + "EmailsRoute";
        public const string SendEmail = Prefix + "/SendEmail";
    } 
    #endregion


    public static class InstructorRouting
    {
        private const string Prefix = Rule + "InstructorRouting";
        public const string GetSalarySummationOfInstructor = Prefix + "/Salary-Summation-Of-Instructor";
        public const string AddInstructor = Prefix + "/Create";
    }

}
