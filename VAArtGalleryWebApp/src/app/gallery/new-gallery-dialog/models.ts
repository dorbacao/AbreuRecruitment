export class NewGallery {

  public id: string;
  public name: string;
  public city: string;
  public manager: string;
  public works: NewWork[] = [];

  constructor(){
    this.id = '';
    this.name = '';
    this.city = '';
    this.manager = '';
    this.works = [];
  }

  addWorks(newWork: NewWork){
    this.works.push(newWork);
  }
  
}

export class NewWork {
  
  constructor(){
    this.id = '';
    this.name = '';
    this.author = '';
    this.creationYear = 0;
    this.askPrice = 0.00;
  }

  public id: string;
  public name: string;
  public author: string;
  public creationYear: number;
  public askPrice: number;
}



