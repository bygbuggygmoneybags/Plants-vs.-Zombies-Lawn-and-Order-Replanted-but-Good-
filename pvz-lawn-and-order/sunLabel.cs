using Godot;
using pvzlawnandorder;


	public partial class sunLabel : Label
	{
		private GameManager _gameManager;

		public override void _Ready()
		{
			_gameManager = GetNode<GameManager>("/root/GameManager");  

			if (_gameManager?.Sun == null)
			{
				GD.PrintErr("Sun not found! Check path.");
				return;
			}

			UpdateScore();  
		}

		public override void _Process(double delta)
		{
			UpdateScore();  
		}

		private void UpdateScore()
		{
			if (_gameManager != null)
			{
				Text = $"Score: {_gameManager.score}";
			}
		}
	}
