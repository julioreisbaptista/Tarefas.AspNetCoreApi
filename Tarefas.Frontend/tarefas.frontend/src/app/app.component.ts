import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { Tarefa } from './tarefa_model';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  
  formulario: FormGroup | any;
  tarefa: any = new Tarefa("", new Date(), 1);
  tarefas: any[] = [];

  constructor(private formBuilder: FormBuilder,
    private http: HttpClient) {
  }

  ngOnInit(): void {
    this.formulario = this.formBuilder.group({
      descricao: ['', Validators.required],
      data: ['', Validators.required],
      status: ['']
    });

    this.formulario.setValue({
      descricao: this.tarefa.descricao,
      data: this.tarefa.data,
      status: this.tarefa.status
    });

    this.BuscaTarefas();
  }

  Salvar() {
    this.tarefa.descricao = this.formulario.get('descricao').value;
    this.tarefa.data = this.formulario.get('data').value;
    this.tarefa.status = this.formulario.get('status').value;

    const httpOptions = {
      headers: new HttpHeaders({
        'Referrer-Policy': 'strict-origin-when-cross-origin'
      })
    };

    this.http.post('https://localhost:7093/Tarefa', this.tarefa, httpOptions).subscribe(
      (resposta) => {
        console.log('Requisição enviada com sucesso!', resposta);
        this.BuscaTarefas();
      },
      (erro) => {
        console.log('Erro ao enviar requisição:', erro);
      }
    );
    console.log(this.tarefa);
  }

  BuscaTarefas(){
    const httpOptions = {
      headers: new HttpHeaders({
        'Referrer-Policy': 'strict-origin-when-cross-origin'
      })
    };
  
    this.http.get<ITarefa[]>('https://localhost:7093/Tarefa', httpOptions).subscribe(
      (resposta: ITarefa[]) => {
        this.tarefas = resposta;
      },
      (erro) => {
        console.log('Erro ao buscar tarefas:', erro);
      }
    );
  }
  title = 'tarefas.frontend';
}

interface ITarefa {
  id: number;
  descricao: string;
  data: Date;
  status: string;
}