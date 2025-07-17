import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { TaskService } from '../../../services/task.service';
import { DetailTaskDto } from '../../../models/DetailTaskDto';
import { TaskState } from '../../../enums/TaskState';

@Component({
  selector: 'app-task-detail',
  standalone: true,
  imports: [CommonModule, MatDialogModule, MatButtonModule],
  templateUrl: './task-detail.component.html',
  styleUrls: []
})
export class TaskDetailComponent implements OnInit {
  task: DetailTaskDto;

  constructor(
    public dialogRef: MatDialogRef<TaskDetailComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { taskId: number },
    private taskService: TaskService
  ) {
    this.task = { id: 0, state: TaskState.Pending, description: '', createdBy: '', isDeleted: false };
  }

  ngOnInit(): void {
    this.taskService.getTaskById(this.data.taskId).subscribe({
      next: (task) => {
        this.task = {
          id: task.id,
          state: task.state,
          description: task.description,
          createdBy: task.createdBy || 'Desconocido',
          isDeleted: task.isDeleted || false
        };
      },
      error: (err) => console.error('Error loading task details:', err)
    });
  }

  onClose(): void {
    this.dialogRef.close();
  }

  getStateLabel(state: number): string {
    switch (state) {
      case TaskState.Pending:
        return 'Pendiente';
      case TaskState.InProgress:
        return 'En Progreso';
      case TaskState.Completed:
        return 'Completada';
      default:
        return 'Desconocido';
    }
  }
}