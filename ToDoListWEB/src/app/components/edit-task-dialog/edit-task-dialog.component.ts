import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { TaskDto } from '../../models/TaskDto';
import { DetailTaskDto } from '../../models/DetailTaskDto';
import { TaskState } from '../../enums/TaskState';
import { TaskService } from '../../services/task.service';

@Component({
  selector: 'app-edit-task-dialog',
  standalone: true,
  imports: [CommonModule, FormsModule, MatDialogModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatSelectModule],
  templateUrl: './edit-task-dialog.component.html',
  styleUrls: []
})
export class EditTaskDialogComponent implements OnInit {
  task: DetailTaskDto;
  editedTask: TaskDto;
  protected readonly TaskState = TaskState;
  taskStates = [
    { value: TaskState.Pending, label: 'Pendiente' },
    { value: TaskState.InProgress, label: 'En Progreso' },
    { value: TaskState.Completed, label: 'Completada' }
  ];

  constructor(
    public dialogRef: MatDialogRef<EditTaskDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { taskId: number },
    private taskService: TaskService
  ) {
    this.task = { id: 0, state: TaskState.Pending, description: '', createdBy: '', isDeleted: false };
    this.editedTask = { id: 0, description: '', state: TaskState.Pending };
  }

  ngOnInit(): void {
    this.taskService.getTaskById(this.data.taskId).subscribe({
      next: (task) => {
        this.task = { ...task };
        this.editedTask = {
          id: task.id,
          description: task.description,
          state: task.state
        };
      },
      error: (err) => console.error('Error loading task for editing:', err)
    });
  }

  onSave(): void {
    if (!this.editedTask.description) {
      console.error('La descripción es requerida');
      return;
    }
    this.taskService.editTask(this.editedTask).subscribe({
      next: () => {
        this.dialogRef.close(this.editedTask);
      },
      error: (err) => console.error('Error saving task:', err)
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}