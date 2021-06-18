
## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

# Dev стенд
ng build -c dev --progress

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.

## Publish Dev
ng build -c dev --progress
scp -r C:\Users\Dmitrii\source\repos\KadOzenka\Source\angular-Web-settings\dist\* miomoko@192.168.3.111:/var/www/miomo_ko/dev/app_feature_toggle
P@ssw0rd
