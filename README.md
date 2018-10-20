# wordpress-installer

Wordpress installer allows you to install Wordpress from command line.

## Getting started
To use this command line tool, please make sure you have the necessary prerequisites.

### Prerequisites
Change the paths in [InstallCommand.cs](wordpress/InstallCommand.cs) to the location of the respective files on your computer.

```
private static string virtualHostsConfFile = @"C:\xampp\apache\conf\extra\httpd-vhosts.conf";
private static string hostsFile = @"C:\Windows\System32\drivers\etc\hosts";
private static string xamppPath = @"C:\xampp\htdocs\";
```

## Installing
Clone this repository, change the directories as mentioned above and compile. Next, add the `bin` to `Environment variables` (Windows).

## Running
* In your terminal, enter `wordpress`
* For example, enter `install [domain1] [domain2]`

## Built With
* Visual Studio 2017

## License
This project is licensed under the GNU GENERAL PUBLIC LICENSE - see the [License.txt](wordpress/License.txt) file for details
