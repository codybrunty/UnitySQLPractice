<?php

$con = mysqli_connect('localhost','root','root','practice');

//Error Codes
//1 - Database Connection Error
//2 - Username Query Failed
//3 - Username Already Exists
//4 - Email Query Failed
//5 - Email Already Exists
//6 - Insert Query Failed

if(mysqli_connect_errno()){
    echo("1");
    exit();
}

//Unity Form
$appPassword = $_POST["apppassword"];
$email = $_POST["email"];
$username = $_POST["username"];
$password = $_POST["password"];

//If request is not sent from app exit
if($appPassword != "thisisplaceholderpassword"){
    exit();
}

$usernamequery = "SELECT username FROM players WHERE username = '".$username."';";
$usernamecheck = mysqli_query($con, $usernamequery) or die("2");

if(mysqli_num_rows($usernamecheck)>0){
    echo("3");
    exit();
}

$emailquery = "SELECT email FROM players WHERE email = '".$email."';";
$emailcheck = mysqli_query($con, $emailquery) or die("4");

if(mysqli_num_rows($emailcheck)>0){
    echo("5");
    exit();
}


$hashPassword = password_hash($password,PASSWORD_DEFAULT);
$insertnewplayerquery = "INSERT INTO players(email, username, password) VALUES ('".$email."','".$username."','".$hashPassword."');";
$insertnewplayercheck = mysqli_query($con, $insertnewplayerquery) or die("6");
echo("0");

$con->close();

?>