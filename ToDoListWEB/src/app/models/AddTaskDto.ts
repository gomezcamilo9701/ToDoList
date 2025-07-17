import { TaskState } from "../enums/TaskState";

export interface AddTaskDto {
  state: TaskState;
  description: string;
}