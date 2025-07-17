import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { TaskService } from '../../../services/task.service';
import { AuthService } from '../../../services/auth.service';
import { AddTaskDto } from '../../../models/AddTaskDto';
import Swal from 'sweetalert2';
import { TaskDto } from '../../../models/TaskDto';
import { TaskListItemComponent } from '../task-list-item/task-list-item.component';
import { EditTaskDialogComponent } from '../edit-task-dialog/edit-task-dialog.component';
import { TaskDetailComponent } from '../task-detail/task-detail.component';
import { TaskState } from '../../../enums/TaskState';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatToolbarModule,
    MatListModule,
    MatIconModule,
    MatSnackBarModule,
    MatSelectModule,
    TaskListItemComponent
  ],
  templateUrl: './task-list.component.html',
  styleUrls: []
})
export class TaskListComponent implements OnInit {
  tasks: TaskDto[] = [];
  newTask: AddTaskDto = { state: TaskState.Pending, description: '' };
  username: string | null = null;
  protected readonly TaskState = TaskState;
  taskStates = [
    { value: TaskState.Pending, label: 'Pendiente' },
    { value: TaskState.InProgress, label: 'En Progreso' },
    { value: TaskState.Completed, label: 'Completada' }
  ];

  constructor(
    private taskService: TaskService,
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.taskService.getAllTasks().subscribe({
      next: (tasks) => this.tasks = tasks,
      error: (err) => this.snackBar.open('Failed to load tasks', 'Close', { duration: 3000 })
    });
  }

  addTask(): void {
    if (!this.newTask.description) {
      Swal.fire('Error', 'La descripción es requerida', 'error');
      return;
    }
    this.taskService.addTask(this.newTask).subscribe({
      next: () => {
        this.newTask = { state: TaskState.Pending, description: '' };
        this.loadTasks();
        Swal.fire('Añadido!', 'La tarea ha sido añadida.', 'success');
      },
      error: (err) => Swal.fire('Error', 'No se pudo añadir la tarea', 'error')
    });
  }

  editTask(id: number): void {
    this.dialog.open(EditTaskDialogComponent, {
      width: '400px',
      data: { taskId: id }
    }).afterClosed().subscribe(result => {
      if (result) {
        this.loadTasks();
        Swal.fire('Actualizado!', 'La tarea ha sido actualizada.', 'success');
      }
    });
  }

  viewTaskDetail(id: number): void {
    this.dialog.open(TaskDetailComponent, {
      width: '400px',
      data: { taskId: id }
    });
  }

  deleteTask(id: number): void {
    Swal.fire({
      title: '¿Estás seguro?',
      text: '¡Esta acción no se puede deshacer!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: '¡Sí, elimínala!',
      cancelButtonText: 'No, mantenla'
    }).then((result) => {
      if (result.isConfirmed) {
        this.taskService.deleteTask(id).subscribe({
          next: () => {
            this.loadTasks();
            Swal.fire('Eliminado!', 'La tarea ha sido eliminada.', 'success');
          },
          error: (err) => Swal.fire('Error', 'No se pudo eliminar la tarea', 'error')
        });
      }
    });
  }

  onLogout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}