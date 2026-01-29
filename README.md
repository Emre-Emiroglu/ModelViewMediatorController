# ModelViewMediatorController
ModelViewMediatorController provides a Model-View-Controller (MVC) architecture for Unity, enhanced with a Mediator pattern to decouple views from logic and state management. The package encourages clean separation of responsibilities and modularity through generic interfaces.

## Features
ModelViewMediatorController includes the following key features:
* MVC Architecture: Clear separation between data (Model), gameobject (View), and logic (Controller).
* Mediator Pattern: Views stay purely visual while Mediators handle signals and controller orchestration.
* Modular Design: Uses generic interfaces for easy extendability and reusability.

## Getting Started
Install via UPM with git URL

`https://github.com/Emre-Emiroglu/ModelViewMediatorController.git`

Clone the repository
```bash
git clone https://github.com/Emre-Emiroglu/ModelViewMediatorController.git
```
This project is developed using Unity version 6000.2.6f2.

## Usage
* Model: Create a model class that holds data.
    ```csharp
    public class MySettings { }
    
    public class MyModel : Model<MySettings>
    {
        public MyModel(MySettings settings) : base(settings) { }
        
        public override void LoadData() { }
        public override void SaveData() { }
    }
    ```

* View: Implement a view that will display the data.
    ```csharp
    public class MyView : View
    {
        public override void Show() => base.Show();
        public override void Hide() => base.Hide();
    }
    ```

* Mediator: The mediator ensures that the view and model do not directly communicate.
    ```csharp
    public class MyMediator : Mediator<MyModel, MySettings, MyView>
    {
        public MyMediator(MyModel model, MyView view) : base(model, view) { }
        
        public override void SetSubscriptions(bool isSubscribed) { }
    }
    ```

* Controller: Implement a controller to manage the flow between the model and view and execute an algorithm.
    ```csharp
    private class MyControllerOne : Controller<MyModel, MySettings, MyView>
    {
        public MyControllerOne(MyModel model, MyView view) : base(model, view) { }
        
        public override void Execute(params object[] parameters) { }
    }
    
    private class MyControllerTwo : Controller<MyModel, MySettings, MyView>
    {
        public MyControllerTwo(MyModel model, MyView view) : base(model, view) { }
        
        public override void Execute(params object[] parameters) { }
    }
    ```

* Generic interfaces: Model, view, mediator, and controller classes inherit generic interfaces written for them.
    ```csharp
    public interface IModel<out TSettings> where TSettings : class
    {
        public TSettings Settings { get; }
        
        public void LoadData();
        
        public void SaveData();
    }
    
    public interface IView
    {
        public void Show();
        
        public void Hide();
    }
    
    public interface IMediator<out TModel, TSettings, out TView>
        where TModel : IModel<TSettings>
        where TSettings : class
        where TView : IView
    {
        public TModel Model { get; }
         
        public TView View { get; }
         
        public void Initialize();
        
        public void Dispose();
        
        public void SetSubscriptions(bool isSubscribed);
    }
    
    public interface IController<out TModel, TSettings, out TView>
        where TModel : IModel<TSettings>
        where TSettings : class
        where TView : IView
    {
        public TModel Model { get; }
        
        public TView View { get; }
        
        public void Execute();
    }
    ```

## Acknowledgments
Special thanks to the Unity community for their invaluable resources and tools.

For more information, visit the GitHub repository.
