using System.Collections.Generic;
using ModelViewMediatorController.Runtime;
using NUnit.Framework;
using UnityEngine;

namespace ModelViewMediatorController.Tests.EditMode
{
    public sealed class ModelViewMediatorControllerEditModeTests
    {
        private sealed class TestSettings
        {
            public int TestValue;
        }
        private sealed class TestModel : Model<TestSettings>
        {
            public int TestValue;
            public TestModel(TestSettings settings) : base(settings) => TestValue = settings.TestValue;
            public override void LoadData() => TestValue = Settings.TestValue;
            public override void SaveData() => Settings.TestValue = TestValue;
        }
        private sealed class TestView : View { }
        private sealed class TestMediator : Mediator<TestModel, TestSettings, TestView>
        {
            public readonly List<IController<TestModel, TestSettings, TestView>> Controllers = new();
            public bool IsSubscribed;
            public TestMediator(TestModel model, TestView view) : base(model, view) => IsSubscribed = false;
            public override void SetSubscriptions(bool isSubscribed) => IsSubscribed = isSubscribed;
        }
        private sealed class TestControllerOne : Controller<TestModel, TestSettings, TestView>
        {
            public readonly TestMediator Mediator;
            public bool IsExecuted;
            public TestControllerOne(TestModel model, TestView view, TestMediator mediator) : base(model, view)
            {
                IsExecuted = false;
                Mediator = mediator;
            }
            public override void Execute(params object[] parameters) => IsExecuted = true;
        }
        private sealed class TestControllerTwo : Controller<TestModel, TestSettings, TestView>
        {
            public readonly TestMediator Mediator;
            public bool IsExecuted;
            public TestControllerTwo(TestModel model, TestView view, TestMediator mediator) : base(model, view)
            {
                IsExecuted = false;
                Mediator = mediator;
            }
            public override void Execute(params object[] parameters) => IsExecuted = true;
        }

        private TestSettings _settings;
        private TestModel _model;
        private TestView _view;
        private TestMediator _mediator;
        private TestControllerOne _controllerOne;
        private TestControllerTwo _controllerTwo;

        [SetUp]
        public void Setup()
        {
            _settings = new TestSettings();
            _model = new TestModel(_settings);
            _view = new GameObject("TestView").AddComponent<TestView>();
            _mediator = new TestMediator(_model, _view);
            _controllerOne = new TestControllerOne(_model, _view, _mediator);
            _controllerTwo = new TestControllerTwo(_model, _view, _mediator);
            
            _mediator.Controllers.Add(_controllerOne);
            _mediator.Controllers.Add(_controllerTwo);
        }

        [Test]
        public void Model_Save_Load_Test()
        {
            _model.TestValue = 5;
            
            _model.SaveData();

            Assert.IsTrue(_settings.TestValue == 5);
            
            _settings.TestValue = 10;
            
            _model.LoadData();
            
            Assert.IsTrue(_model.TestValue == 10);
        }

        [Test]
        public void Mediator_Model_Relationship_Test()
        {
            _mediator.Model.TestValue = 15;
            
            _mediator.Model.SaveData();

            Assert.IsTrue(_mediator.Model.Settings.TestValue == 15);
            
            _mediator.Model.Settings.TestValue = 20;
            
            _mediator.Model.LoadData();
            
            Assert.IsTrue(_mediator.Model.TestValue == 20);
        }

        [Test]
        public void Mediator_View_Relationship_Test()
        {
            _mediator.View.Show();
            
            Assert.AreEqual(_view.gameObject.activeInHierarchy, true);
            
            _mediator.View.Hide();
            
            Assert.AreEqual(_view.gameObject.activeInHierarchy, false);
        }
        
        [Test]
        public void Mediator_Controllers_Relationship_Test()
        {
            Assert.IsTrue(_mediator.Controllers.Count == 2);
            
            _mediator.Controllers.ForEach(x => x.Execute());
            
            Assert.AreEqual(_controllerOne.IsExecuted, true);
            Assert.AreEqual(_controllerTwo.IsExecuted, true);
        }
        
        [Test]
        public void Controllers_Model_Relationship_Test()
        {
            _controllerOne.Model.TestValue = 25;
            
            _controllerOne.Model.SaveData();

            Assert.IsTrue(_controllerOne.Model.Settings.TestValue == 25);
            
            _controllerOne.Model.Settings.TestValue = 30;
            
            _controllerOne.Model.LoadData();
            
            Assert.IsTrue(_controllerOne.Model.TestValue == 30);
            
            _controllerTwo.Model.TestValue = 35;
            
            _controllerTwo.Model.SaveData();

            Assert.IsTrue(_controllerTwo.Model.Settings.TestValue == 35);
            
            _controllerTwo.Model.Settings.TestValue = 40;
            
            _controllerTwo.Model.LoadData();
            
            Assert.IsTrue(_controllerTwo.Model.TestValue == 40);
        }
        
        [Test]
        public void Controllers_View_Relationship_Test()
        {
            _controllerOne.View.Show();
            
            Assert.AreEqual(_view.gameObject.activeInHierarchy, true);
            
            _controllerOne.View.Hide();
            
            Assert.AreEqual(_view.gameObject.activeInHierarchy, false);
            
            _controllerTwo.View.Show();
            
            Assert.AreEqual(_view.gameObject.activeInHierarchy, true);
            
            _controllerTwo.View.Hide();
            
            Assert.AreEqual(_view.gameObject.activeInHierarchy, false);
        }

        [Test]
        public void Controller_Mediator_Relationship_Test()
        {
            _controllerOne.Mediator.Initialize();
            
            Assert.AreEqual(_mediator.IsSubscribed, true);
            
            _controllerOne.Mediator.Dispose();
            
            Assert.AreEqual(_mediator.IsSubscribed, false);
            
            _controllerTwo.Mediator.Initialize();
            
            Assert.AreEqual(_mediator.IsSubscribed, true);
            
            _controllerTwo.Mediator.Dispose();
            
            Assert.AreEqual(_mediator.IsSubscribed, false);
        }
    }
}