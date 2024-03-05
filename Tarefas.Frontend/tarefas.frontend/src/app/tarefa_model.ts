export class Tarefa {
    descricao: string;
    data: Date;
    status: Number;
  
    constructor(descricao: string, data: Date, status: Number) {
      this.descricao = descricao;
      this.data = data;
      this.status = status;
    }

  }