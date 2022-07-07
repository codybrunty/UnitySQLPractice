<?php

$con = mysqli_connect('localhost','root','root','practice');

//Error Codes
//1 - Database Connection Error
//2 - Update Query Failed

if(mysqli_connect_errno()){
    echo("1");
    exit();
}

//Unity Form
$appPassword = $_POST["apppassword"];
$username = $_POST["username"];
$score = $_POST["score"];

//If request is not sent from app exit
if($appPassword != "thisisplaceholderpassword"){
    exit();
}

$updateScoreQuery = "UPDATE players SET score = '".$score."' WHERE username = '".$username."';";
$updateScoreCheck = mysqli_query($con, $updateScoreQuery) or die("2");

echo("score:".$score);

$con->close();

?>