namespace SharedModels.Enum
{
    public enum ErrorCodes
    {
        CHANGES_NOT_SAVED = 100,
        TASK_IS_OPEN = 101,
        PROJECT_NOT_FOUND = 102,
        USER_NOT_FOUND = 103,
        EMPLOYEE_IS_IN_PROJECT = 104,
        EMPLOYEE_ISNT_IN_PROJECT = 105,
        INVALID_REQUEST = 106,
        ROLE_NOT_FOUND = 107,
        PROJECTS_NOT_FOUND = 108,
        TASKS_NOT_FOUND = 109,
        TASK_NOT_FOUND = 110,
        USER_RESTRICTED = 111,
        USER_HAS_OPENED_TASKS = 112,
        TASK_IS_DONE = 113,
        VALID_REQUEST = 200,
        UNAUTHORIZED = 403,
        RECORD_NOT_FOUND = 404,
        BAD_REQUEST = 500
    }
}
