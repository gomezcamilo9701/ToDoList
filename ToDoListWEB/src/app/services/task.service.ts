import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { TaskDto } from '../models/TaskDto';
import { AddTaskDto } from '../models/AddTaskDto';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = `${environment.apiUrl}/task`;
  
  constructor(private http: HttpClient, private authService: AuthService) {}

  private getHeaders(): HttpHeaders {
    const token = this.authService.getToken();
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }

  getAllTasks(): Observable<TaskDto[]> {
    return this.http.get<TaskDto[]>(`${this.apiUrl}/getAll`, { headers: this.getHeaders() });
  }

  getTaskById(id: number): Observable<TaskDto> {
    return this.http.get<TaskDto>(`${this.apiUrl}/getById?id=${id}`, { headers: this.getHeaders() });
  }

  addTask(task: AddTaskDto): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/add`, task, { headers: this.getHeaders() });
  }

  editTask(task: TaskDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/edit`, task, { headers: this.getHeaders() });
  }

  deleteTask(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/delete?id=${id}`, { headers: this.getHeaders() });
  }
}