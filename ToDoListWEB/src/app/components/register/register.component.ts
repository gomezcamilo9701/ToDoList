import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { AuthService } from '../../services/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    FormsModule,
    RouterModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatToolbarModule,
    MatSnackBarModule
  ],
  templateUrl: './register.component.html',
  styleUrls: []
})
export class RegisterComponent {
  registerDto = { username: '', password: '' };

  constructor(
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  onRegister(): void {
    this.authService.register(this.registerDto).subscribe({
      next: () => {
        Swal.fire('¡Éxito!', 'Registro exitoso! Por favor, inicia sesión.', 'success').then(() => {
          this.router.navigate(['/login']);
        });
      },
      error: (err) => {
        Swal.fire('Error', err.error || 'El registro falló', 'error');
      }
    });
  }
}