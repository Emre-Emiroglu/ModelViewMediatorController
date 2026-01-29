using System.Collections;
using System.Collections.Generic;
using ModelViewMediatorController.Runtime;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace ModelViewMediatorController.Tests.PlayMode
{
    public sealed class ModelViewMediatorControllerPlayModeTests
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
        
        [UnitySetUp]
        public IEnumerator Setup()
        {
            _settings = new TestSettings();
            _model = new TestModel(_settings);
            _view = new GameObject("TestView").AddComponent<TestView>();
            _mediator = new TestMediator(_model, _view);
            _controllerOne = new TestControllerOne(_model, _view, _mediator);
            _controllerTwo = new TestControllerTwo(_model, _view, _mediator);
            
            _mediator.Controllers.Add(_controllerOne);
            _mediator.Controllers.Add(_controllerTwo);

            yield return null;
        }
        
        [UnityTest]
        public IEnumerator Model_Save_And_Load_Test()
        {
            _model.TestValue = 5;
            
            _model.SaveData();
            
            yield return null;
            
            Assert.AreEqual(5, _settings.TestValue);

            _settings.TestValue = 10;
            
            _model.LoadData();
            
            yield return null;
            
            Assert.AreEqual(10, _model.TestValue);
        }
        
        [UnityTest]
        public IEnumerator Mediator_Model_Relationship_Test()
        {
            _mediator.Model.TestValue = 15;
            
            _mediator.Model.SaveData();
            
            yield return null;
            
            Assert.AreEqual(15, _mediator.Model.Settings.TestValue);

            _mediator.Model.Settings.TestValue = 20;
            
            _mediator.Model.LoadData();
            
            yield return null;
            
            Assert.AreEqual(20, _mediator.Model.TestValue);
        }

        [UnityTest]
        public IEnumerator Mediator_View_Relationship_Test()
        {
            _mediator.View.Show();
            
            yield return null;
            
            Assert.IsTrue(_view.gameObject.activeInHierarchy);

            _mediator.View.Hide();
            
            yield return null;
            
            Assert.IsFalse(_view.gameObject.activeInHierarchy);
        }

        [UnityTest]
        public IEnumerator Mediator_Controllers_Relationship_Test()
        {
            Assert.AreEqual(2, _mediator.Controllers.Count);
            
            _mediator.Controllers.ForEach(x => x.Execute());
            
            yield return null;
            
            Assert.IsTrue(_controllerOne.IsExecuted);
            Assert.IsTrue(_controllerTwo.IsExecuted);
        }

        [UnityTest]
        public IEnumerator Controllers_Model_Relationship_Test()
        {
            _controllerOne.Model.TestValue = 25;
            
            _controllerOne.Model.SaveData();
            
            yield return null;
            
            Assert.AreEqual(25, _controllerOne.Model.Settings.TestValue);

            _controllerOne.Model.Settings.TestValue = 30;
            
            _controllerOne.Model.LoadData();
            
            yield return null;
            
            Assert.AreEqual(30, _controllerOne.Model.TestValue);

            _controllerTwo.Model.TestValue = 35;
            
            _controllerTwo.Model.SaveData();
            
            yield return null;
            
            Assert.AreEqual(35, _controllerTwo.Model.Settings.TestValue);

            _controllerTwo.Model.Settings.TestValue = 40;
            
            _controllerTwo.Model.LoadData();
            
            yield return null;
            
            Assert.AreEqual(40, _controllerTwo.Model.TestValue);
        }

        [UnityTest]
        public IEnumerator Controllers_View_Relationship_Test()
        {
            _controllerOne.View.Show();
            
            yield return null;
            
            Assert.IsTrue(_view.gameObject.activeInHierarchy);

            _controllerOne.View.Hide();
            
            yield return null;
            
            Assert.IsFalse(_view.gameObject.activeInHierarchy);

            _controllerTwo.View.Show();
            
            yield return null;
            
            Assert.IsTrue(_view.gameObject.activeInHierarchy);

            _controllerTwo.View.Hide();
            
            yield return null;
            
            Assert.IsFalse(_view.gameObject.activeInHierarchy);
        }

        [UnityTest]
        public IEnumerator Controller_Mediator_Relationship_Test()
        {
            _controllerOne.Mediator.Initialize();
            
            yield return null;
            
            Assert.IsTrue(_mediator.IsSubscribed);

            _controllerOne.Mediator.Dispose();
            
            yield return null;
            
            Assert.IsFalse(_mediator.IsSubscribed);

            _controllerTwo.Mediator.Initialize();
            
            yield return null;
            
            Assert.IsTrue(_mediator.IsSubscribed);

            _controllerTwo.Mediator.Dispose();
            
            yield return null;
            
            Assert.IsFalse(_mediator.IsSubscribed);
        }
    }
}