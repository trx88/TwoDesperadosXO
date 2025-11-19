# TwoDesperados Tic-Tac-Toe
Job application coding challenge
# Code design & decisions
This section describes the most important elements used in implementing the game.
## Game/Scene Context (Replacement for Zenject)
GameContext and SceneContext (inherited by PlaySceneContext and GameSceneContext) classes are used to mimic the the behaviour of Zenject to some extent. They're used throughout the project to inject services and factories, to set up repositories, and initialize the UI state machine. It eases the usage of mentioned entities.

## Services
A couple of services were implemented to help with the separation of concerns:
* MatchService - used to handle match logic outside of UI logic.
* CounterService - used to count the time for each match. It's run on a separate GameObject runner to keep the logic outside of MonoBehaviour.
* SceneService - used to switch between the scenes.

## Repository
### Base Repository
Repository was implemented as well, but with twist - separating data by storage type (InMemory & PlayerPrefs). Although an overkill for this scope, I'm used to using Repository since it can be used not just for storage, but for data change callback as well. Also, it ties perfectly into UI's MVVM.

Base Repository is implemented in a standard way (with CRUD methods) with addition of data change Actions other classes subscribe to. Specific repositories are InMemoryRepository (data is stored in-memory and disappear when game is closed) and PlayerPrefsRepository that is storing data in PlayerPrefs. Each repository can initialize using an initialization action for specific data model (basically set default values and/or load stored data in case of PlayerPrefsRepository).

### Repository Factories
RepositoryFactory (and each of it's specific variants) is used to create or get a repository for specific data model. In order to get or create a repository for a specific data model, RepositoryConfig must be created for that specific data model that consists of a type and an optional InitializationAction.
All created repositories are stored in the IServiceContainer (System.ComponentModel.Design), since it stores services using types (repositories are read by type as well). Both factories are installed in the GameContext, and their configurations added along with initialization actions.
By using this approach, each specific RepositoryFactory can be injected where needed and RepositoryOf<> method used get access to a desired repository. 

### Initialization Actions
Like it was mentioned above, an InitializationAction can be used to initialize a repository. This boils down to using Repository's Create method to set default values for the data model, and by doing so create a repository for that data model. It makes sense to always use InitializationAction with PlayerPrefsRepository, since PlayerPrefs data should have a default value when the game starts. InMemoryRepository repositories can be initialized during gameplay, since data can come from variety of places (in-game logic, API calls, etc.)

## UI & MVVM
MVVM became the standard when data driven UI needs to be implemented.
### Model
Model layer is already solved by using Repository. This implementation is not using the Model for model logic, and domain-specific logic lives in the services.
### View
Each View has a set of methods that are called on OnEnabled() that constructs the injected ViewModel, sets data bindings, sets action callbacks, initializes the UI elements and ViewComponents (repetative building blocks) and finalizes the View. View only reacts to data changes and handles UI logic (view transitions, setting the UI elements and animations).
### ViewModel
ViewModel is where all the logic should live. ViewModel is tasked with getting the data, do any logic, and trigger the View to show the needed data. To help with this, a Bindable<T> was implemented. A Bindable binds to an Action provided by the View. When the Bindable data is set, provided Action is invoked triggering the View and notifying it what has changed. Actual UI elements are updated inside that callback method.

## 3rd party packages
* <a href="https://github.com/applejag/Newtonsoft.Json-for-Unity">Newtonsoft Json</a>
* <a href="https://github.com/laicasaane/unity-addressables-manager">AddressablesManager</a>
* <a href="https://github.com/jackyyang09/Simple-Unity-Audio-Manager">AudioManager</a>

## Notes
Run the game from the PlayScene only to avoid the errors with AudioManager missing from the GameScene (it's a DontDestroyOnLoad singleton, so it'll get there 😄).

### Audio files missing
If this happens, please rebuild the AudioLibrary shown in the screenshot.
<img width="1427" height="1107" alt="image" src="https://github.com/user-attachments/assets/3c1b936a-1cab-47e1-a5a6-1489ff52f366" />


## Screenshot
<img width="580" height="1256" alt="image" src="https://github.com/user-attachments/assets/5e2126a2-a818-4cfd-8481-358159169d42" />
