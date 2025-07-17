import { TaskState } from "../enums/TaskState";

export interface TaskDto {
  id: number;
  state: TaskState;
  description: string;
}