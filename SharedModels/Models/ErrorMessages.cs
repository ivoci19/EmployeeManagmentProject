namespace SharedModels.Models
{
    public class ErrorMessages
    {
        public static string RECORD_NOT_FOUND = "Record not found!";
        public static string CHANGES_NOT_SAVED = "Changes are not saved in the database";
        public static string TASK_IS_OPEN = "Project has open tasks";
        public static string PROJECT_NOT_FOUND = "Project not found";
        public static string USER_NOT_FOUND = "User not found";
        public static string EMPLOYEE_IS_IN_PROJECT = "Employee is already assigned in project";
        public static string EMPLOYEE_ISNT_IN_PROJECT = "You aren't assigned to this project";
        public static string INVALID_REQUEST = "You can't delete this user";
        public static string ROLE_NOT_FOUND = "Role not found";
        public static string TASK_NOT_FOUND = "Task not found";
        public static string PROJECTS_NOT_FOUND = "The authorized employee doesn't have any project";
        public static string TASKS_NOT_FOUND = "The projects of the authorized employee don't have any tasks";
        public static string USER_RESTRICTED = "You aren't part of the project";
        public static string UNAUTHORIZED = "You aren't authorized for this option";
        public static string USER_HAS_OPENED_TASKS = "This user can't be deleted because he has opened tasks";
        public static string TASK_IS_DONE = "This task status cannot be changed because is already done";
    }
}
