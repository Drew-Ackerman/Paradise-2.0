<?php
if(!isset($_POST['submit']))
{
	//This page should not be accessed directly. Need to submit the form.
	echo "error; you need to submit the form!";
}
$firstName = $_POST['firstName'];
$lastName = $_POST['lastName'];
$phone = $_POST['phone'];
$visitor_email = $_POST['email'];
$message = $_POST['message'];

//Validate first
if(empty($firstName)||empty($lastName)||empty($visitor_email)) 
{
    echo "Name and email are mandatory!";
    exit;
}



$email_from = $visitor_email;//<== visitor email address
$email_subject = "New Form submission";
$email_body = "You have received a new message from the user $firstName $lastName.\n".
    "Here is the message:\n $message".
    
$to = "faithsatterthwaite@weber.edu";//<== update the email address to faithsatterthwaite@weber.edu later
$headers = "From: $email_from \r\n";
$headers .= "Reply-To: $visitor_email \r\n";
//Send the email!
mail($to,$email_subject,$email_body,$headers);
//done. redirect to thank-you page.
header('Location: secondary.html?success=true');


// Function to validate against any email injection attempts
function IsInjected($str)
{
  $injections = array('(\n+)',
              '(\r+)',
              '(\t+)',
              '(%0A+)',
              '(%0D+)',
              '(%08+)',
              '(%09+)'
              );
  $inject = join('|', $injections);
  $inject = "/$inject/i";
  if(preg_match($inject,$str))
    {
    return true;
  }
  else
    {
    return false;
  }
}
   
?> 