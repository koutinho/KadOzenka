import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';


export class Settings {
  static apiUrl = 'http://localhost:5050/';
//   public load(http: HttpClient) {
//     return http
//       .get<Settings>('assets/config.json', { observe: 'response' })
//       .pipe(
//         tap(response => {
//           if (response.status === 200) {
//             const config: IConfig = response.body || {apiUrl: ''};
//             this.apiUrl = config.apiUrl;
//           }
//         })
//       )
//       .toPromise();
//   }
}
//export const settings: Settings = new Settings();