import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { RouterModule } from '@angular/router';
import { TaskService } from '../../../services/task.service';
import { TaskState } from '../../../enums/TaskState';
import { TaskDto } from '../../../models/TaskDto';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MatCardModule, RouterModule],
  templateUrl: './dashboard.component.html',
  styleUrls: []
})
export class DashboardComponent implements OnInit {
  totalTasks: number = 0;
  completedTasks: number = 0;
  pendingTasks: number = 0;
  inProgressTasks: number = 0;

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.loadDashboardMetrics();
  }

  loadDashboardMetrics(): void {
    this.taskService.getAllTasks().subscribe({
      next: (tasks: TaskDto[]) => {
        this.totalTasks = tasks.length;
        this.completedTasks = tasks.filter(task => task.state === TaskState.Completed).length;
        this.pendingTasks = tasks.filter(task => task.state === TaskState.Pending).length;
        this.inProgressTasks = tasks.filter(task => task.state === TaskState.InProgress).length;
      },
      error: (err) => console.error('Error loading dashboard metrics:', err)
    });
  }
}