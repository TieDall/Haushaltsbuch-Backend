[![build](https://github.com/TieDall/Haushaltsbuch-Backend/actions/workflows/build.yml/badge.svg?branch=master)](https://github.com/TieDall/Haushaltsbuch-Backend/actions/workflows/build.yml)

# Getting Started

## Install Apache, PHP, MariaDB

```
sudo apt install apache2 -y
sudo apt install php -y
cd /var/www/html
sudo rm index.html
sudo service apache2 restart
sudo apt install mariadb-server php-mysql -y
sudo service apache2 restart
```

Configure

```
sudo mysql_secure_installation
enter
y
set password
y
y
y
y
```

Configure MySQL:

```
sudo nano /etc/mysql/mariadb.conf.d/50-server.cnf
comment line with: bind-address = 127.0.0.1
```

```
sudo service mysql restart
```

Add user for application:

```
sudo mysql --user=root --password

CREATE USER 'username'@'localhost' IDENTIFIED BY 'password';
CREATE USER 'username'@'%' IDENTIFIED BY 'password';

GRANT ALL ON *.* TO 'username'@'localhost';
GRANT ALL ON *.* TO 'username'@'%';

FLUSH PRIVILEGES;
exit;
```

```
sudo phpenmod mysqli
sudo apt install phpmyadmin -y
enter
enter
set password
sudo ln -s /usr/share/phpmyadmin /var/www/html/phpmyadmin

```

## Install .NET

```
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin
echo 'export DOTNET_ROOT=$HOME/.dotnet' >> ~/.bashrc
echo 'export PATH=$PATH:$HOME/.dotnet' >> ~/.bashrc
source ~/.bashrc
sudo reboot
```
source: https://docs.microsoft.com/de-de/dotnet/iot/deployment

## Install App

Datenbank anlegen unter [IP]/phpmyadmin mit dem Namen Haushaltsbuch
Clone Repository
MySqlHaushaltsbuchContext bearbeiten. IP, User und Passwort setzen
Program.cs bearbeitem: IP, User, Passwort setzen
"MySql" => options.UseMySQL("server=192.168.198.37;database=Haushaltsbuch;user=hhuser;password=git1003hub")
Setzen:  var provider = configuration.GetValue("Provider", "MySql");

Migrations ausführen:


## App starten

```
dotnet run --urls=http://0.0.0.0:44304/ --project [path to project]/Haushaltsbuch-Backend/WebApi/WebApi.csproj
```
