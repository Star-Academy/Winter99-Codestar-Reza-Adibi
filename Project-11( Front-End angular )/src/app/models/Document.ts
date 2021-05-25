export class TextDocument {
  public id!: String;
  public content!: String;

  constructor(id: String, content: String) {
    this.id = id;
    this.content = content;
  }
}
