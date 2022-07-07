<?php

$con = mysqli_connect('localhost','root','root','practice');

//Error Codes
//1 - Database Connection Error
//2 - Username Query Failed
//3 - Username Error
//4 - Password Error

if(mysqli_connect_errno()){
    echo("1");
    exit();
}

//Unity Form
$appPassword = $_POST["apppassword"];
$username = $_POST["username"];
$password = $_POST["password"];

//If request is not sent from app exit
if($appPassword != "thisisplaceholderpassword"){
    exit();
}

$usernamequery = "SELECT * FROM players WHERE username = '".$username."';";
$usernamecheck = mysqli_query($con, $usernamequery) or die("2");

if($usernamecheck->num_rows != 1){
    echo("3");
    exit();
}
else{
    $playerData = mysqli_fetch_assoc($usernamecheck);
    $playerDataPassword = $playerData["password"];
    
    if(password_verify($password,$playerDataPassword)){
        $playerDataUsername = $playerData["username"];
        $playerDataScore = $playerData["score"];
        echo($playerDataUsername.":".$playerDataScore);
    }
    else{
        echo("4");
    }

    $con->close();

}

?>