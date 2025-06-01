export interface IGallery {
  name: string;
  city: string;
  manager: string;
}

export interface Gallery extends IGallery {
  id: string;
  name: string;
  city: string;
  manager: string;
  nbrOfArtWorksOnDisplay: number;
}

export class NewGallery implements IGallery {

  public name: string;
  public city: string;
  public manager: string;
  public createArtWorkRequest: NewWork[] = [];

  constructor(){
    this.name = 'Marcus';
    this.city = 'Almada';
    this.manager = 'Marcus';
    this.createArtWorkRequest = [];
  }
  
  addWorks(newWork: NewWork){
    this.createArtWorkRequest.push(newWork);
  }

  removeWork(index: number){
    this.createArtWorkRequest.splice(index,1);
  }
  
}

export class NewWork {
  
  constructor(){
    this.name = 'sdfgsdfg';
    this.author = 'sdfgsdgfsdfg';
    this.creationYear = 1986;
    this.askPrice = 654654.00;
  }

  public name: string;
  public author: string;
  public creationYear: number;
  public askPrice: number;
}



