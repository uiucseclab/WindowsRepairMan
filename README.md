# WindowsRepairMan
Windows Repair Man is RansomeWare which targets machines running Windows systems.

## Requirements
Target systems must have .NET framework version 4.0

## How it works
The application is named to trick unsuspecting users into downloading it onto their computers to help fix system problems. It then establishes a connection with a remote key server to retrieve its RSA public key. The application then encrypts important files in the User's directory using AES encryption with a randomly generated password and uses the RSA public key to encrypt the password before sending it to the key server and deleting from memory so there are no traces of the key on the system.  The only way for the user to unlock their files is to make payment and retrieve the password used for the AES encryption. They do this by going to the aforementioned remote key server. The URL for our dummy version of the remote key server is: https://final-460.herokuapp.com/. 


