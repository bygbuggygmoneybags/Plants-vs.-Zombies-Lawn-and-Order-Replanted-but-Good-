# login_screen.gd
extends Control

signal interacted()
@onready var usernameInput = get_node('UsernamePanel')
@onready var passwordInput = get_node('PasswordPanel')
@onready var loginButton = get_node('LoginPanel')
@onready var createAccountButton = get_node('CreateAccountPanel') 

func _ready():
	loginButton.pressed.connect(on_login_pressed)
	createAccountButton.pressed.connect(on_create_account_pressed)

func on_login_pressed():
	var username = usernameInput.text
	var password = passwordInput.text
	
	# In a real system, you'd retrieve hashed password and salt from storage
	# and compare it to the hashed input password.
	if username == "test" and password == "password":
		print("Login successful!")

	else:
		print("Invalid username or password.")

func on_create_account_pressed():
	var username = usernameInput.text
	var password = passwordInput.text
	
	if username.is_empty() or password.is_empty():
		print("Username and password cannot be empty.")
		return
	
	# In a real system, you'd hash and salt the password before storing.
	print("account created")
	# Store username and hashed password securely.
