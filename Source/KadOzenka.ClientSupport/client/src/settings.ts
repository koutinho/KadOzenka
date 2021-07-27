import { environment } from "./environments/environment";

export class Settings {
    static apiUrl = environment.apiUrl;
    static apiDomain = environment.apiDomain;
  }