export abstract class Hidable {
  private readonly displayType: string;
  private faidingDuration: number = 300;
  public opacity: number = 0;
  public display: string = 'none';

  constructor(displayType: string) {
    this.displayType = displayType;
  }

  show() {
    this.display = this.displayType;
    setTimeout(() => {
      this.opacity = 100;
    }, this.faidingDuration);
  }

  hide() {
    this.opacity = 0;
    setTimeout(() => {
      this.display = 'none';
    }, this.faidingDuration);
  }
}
