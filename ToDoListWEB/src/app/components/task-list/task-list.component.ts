import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { TaskService } from '../../services/task.service';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { AddTaskDto } from '../../models/AddTaskDto';

interface Task {
  id: number;
  title: string;
  description: string;
  isCompleted: boolean;
}

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatToolbarModule,
    MatListModule,
    MatIconModule,
    MatSnackBarModule
  ],
  templateUrl: './task-list.component.html',
  styleUrls: []
})
export class TaskListComponent implements OnInit {
  tasks: Task[] = [];
  newTask: AddTaskDto = { state: 0, description: '' };

  constructor(
    private taskService: TaskService,
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar
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
    this.taskService.addTask(this.newTask).subscribe({
      next: () => {
        this.newTask = { state:  0, description: '' };
        this.loadTasks();
        this.snackBar.open('Task added', 'Close', { duration: 3000 });
      },
      error: (err) => this.snackBar.open('Failed to add task', 'Close', { duration: 3000 })
    });
  }

  editTask(task: Task): void {
    this.taskService.editTask(task).subscribe({
      next: () => {
        this.loadTasks();
        this.snackBar.open('Task updated', 'Close', { duration: 3000 });
      },
      error: (err) => this.snackBar.open('Failed to update task', 'Close', { duration: 3000 })
    });
  }

  deleteTask(id: number): void {
    this.taskService.deleteTask(id).subscribe({
      next: () => {
        this.loadTasks();
        this.snackBar.open('Task deleted', 'Close', { duration: 3000 });
      },
      error: (err) => this.snackBar.open('Failed to delete task', 'Close', { duration: 3000 })
    });
  }

  onLogout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}