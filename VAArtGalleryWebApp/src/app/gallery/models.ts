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
  public artWorkRequest: NewWork[] = [];

  constructor(){
    this.name = '';
    this.city = '';
    this.manager = '';
    this.artWorkRequest = [];
  }
  
  addWorks(newWork: NewWork){
    this.artWorkRequest.push(newWork);
  }

  removeWork(index: number){
    this.artWorkRequest.splice(index,1);
  }
  
}

export class NewWork {
  
  constructor(){
    this.name = '';
    this.author = '';
    this.creationYear = 0;
    this.askPrice = 0.00;
  }

  public name: string;
  public author: string;
  public creationYear: number;
  public askPrice: number;
}



