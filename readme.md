# ZBAppReminder

## Overview
ZBAppReminder is an integration with a Vendor software called Customer Appointment Manager. The vendor software uses an Access Databse to manage all data. ZBAppReminder reads the contents of the database to send text reminders to customers about their appointments and to schedule zoom meetings with customers (due to COVID).

## Dependencies
* .Net Framework 4.6.1
* Twilio SDK 5.9.2.0
* Visual Studio 2019+
* Zoom Rest API v2


## Setup
* Settings File
  * Create `settings.json` file in project directory (See Settings section)
  * By default scripts will look in their working directory for this file
  * Settings file can be passed as command line argument (recommended). This can be configured by for debugging in project settings. Ex. `START AppointmentReminder.Meetings.exe ../../../settings.json`
* Build
  * Build using Visual Studio 

## Settings File
The settings file includes all configuration needed to run the project. See `/settings.example.json` file for an example file.

Property | Description
------------ | -------------
TwilioAccountSID | Account ID API Key from Twilio
TwilioAuthToken | Account API Secret Key from Twilio
TwilioFromPhone | Phone number from Twilio used to send text messages
CAPDBPath | File path to Customer Appointment Manager Access Database
ErrorLogPhone | Phone number to send error messages from Reminder
ReminderMsg | Reminder text message text. Use string interpolation for arguments. Ex. `Appointment with {0} is at {1}` will result in `Appointment with John is at 4PM`
ZoomAPIKey | Zoom account API Key
ZoomAPISecret | Zoom account API Secret
CacheFilePath | File path to cache json file. Meeting app will use this to store data about zoom meetings. File will be created if it does not exist.
MeetingCreatedMsg | Appointment created text message text. Use string interpolation for arguments. Ex. `Appointment created with {0} is at {1} with zoom link {2} and password {3}`
MeetingUpdateMsg | Appointment created text message text. Use string interpolation for arguments. Ex. `Appointment updated with {0} is at {1} with zoom link {2} and password {3}`
MeetingDeletedMsg | Appointment deleted text message text. Use string interpolation for arguments. Ex. `Appointment at {0} has been deleted`
