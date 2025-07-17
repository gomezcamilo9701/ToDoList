import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { TaskDto } from '../../models/TaskDto';
import { TaskState } from '../../enums/TaskState'; // Importar el enum

@Component({
  selector: 'app-task-list-item',
  standalone: true,
  imports: [CommonModule, MatListModule, MatIconModule, MatButtonModule],
  templateUrl: './task-list-item.component.html',
  styleUrls: []
})
export class TaskListItemComponent {
  @Input() task!: TaskDto;
  @Output() viewDetail = new EventEmitter<number>();
  @Output() edit = new EventEmitter<number>();
  @Output() delete = new EventEmitter<number>();

  protected readonly TaskState = TaskState;

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