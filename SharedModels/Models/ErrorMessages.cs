namespace SharedModels.Models
{
    public class ErrorMessages
    {
        public static string RECORD_NOT_FOUND = "Record not found!";
        public static string CHANGES_NOT_SAVED = "Changes are not saved in the database!";
        public static string PROJECT_HAS_OPEN_TASKS = "This project cannot be deleted because it has open tasks!";
        public static string PROJECT_NOT_FOUND = "Project not found!";
        public static string USER_NOT_FOUND = "User not found!";
        public static string EMPLOYEE_IS_IN_PROJECT = "Employee is already assigned in project!";
        public static string EMPLOYEE_ISNT_IN_PROJECT = "You aren't part of the project!";
        public static string INVALID_REQUEST = "The administrator cannot be deleted !";
        public static string ROLE_NOT_FOUND = "Role not found!";
        public static string TASK_NOT_FOUND = "Task not found!";
        public static string PROJECTS_NOT_FOUND = "The authorized employee doesn't have any project!";
        public static string TASKS_NOT_FOUND = "The projects of the authorized employee don't have any tasks!";
        public static string USER_RESTRICTED = "You aren't part of the project!";
        public static string UNAUTHORIZED = "You aren't authorized for this option!";
        public static string USER_HAS_OPENED_TASKS = "This user can't be deleted because he has opened tasks!";
        public static string TASK_IS_DONE = "This task status cannot be changed because is already done!";
        public static string TASK_IS_ASSIGNED_TO_ANOTHER_EMPLOYEE = "This task is assigned to another employee!";
        public static string TASK_IS_ALREADY_ASSIGNED = "This task is already assigned to this employee!";
        public static string EMPLOYEE_NOT_PART_OF_PROJECT = "You can't assign employee to task because he is not part of the task project!";
        public static string TASK_CREATED_BY_ANOTHER_USER = "You can't assign employee to task because you haven't created this task!";
        public static string TASK_IS_OPEN = "You can't delete this task because it isn't done yet!";
        public static string USERNAME_ALREADY_USED = "This username is already used!";
        public static string EMAIL_ALREADY_USED = "This email is already used!";
        public static string CODE_ALREADY_USED = "This project code is already used!";
        public static string EMPLOYEE_NOT_FOUND = "Employee not found!";
        public static string EMPLOYEE_HAS_OPENED_TASKS = "This employee cannot be deleted from project because he has opened tasks!";
        public static string SERVER_ERROR = "Server error!";
    }
}
