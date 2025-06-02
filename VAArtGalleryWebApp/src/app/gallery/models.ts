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

  public id: string;
  public name: string;
  public city: string;
  public manager: string;
  public artWorks: NewWork[] = [];

  constructor(){
    this.id = '';
    this.name = '';
    this.city = '';
    this.manager = '';
    this.artWorks = [];
  }
    
}

export class NewWork {
  
  constructor(){
    this.name = '';
    this.author = '';
    this.creationYear = '';
    this.askPrice = '';
  }

  public name: string;
  public author: string;
  public creationYear: string;
  public askPrice: string;
}



