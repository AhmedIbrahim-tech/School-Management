﻿namespace Data.AppMetaData;

public static class Router
{
    #region Const Params
    public const string root = "Api";
    public const string vrsion = "v1";
    public const string Rule = root + "/" + vrsion + "/";
    public const string SingleRoute = "{id}";
    #endregion

    #region Student
    public static class Student
    {
        public const string Prefix = Rule + "Student/";

        public const string Search = Prefix + "Search";
        public const string GetById = Prefix + "GetByID" + "/" + SingleRoute;
        public const string Create = Prefix + "Create";
        public const string Edit = Prefix + "Edit";
        public const string Delete = Prefix + "Delete" + "/" + SingleRoute;

    }
    #endregion
}
