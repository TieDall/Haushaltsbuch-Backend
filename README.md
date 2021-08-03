[![build](https://github.com/TieDall/Haushaltsbuch-Backend/actions/workflows/build.yml/badge.svg?branch=master)](https://github.com/TieDall/Haushaltsbuch-Backend/actions/workflows/build.yml)

# Getting Started

## Install MariaDB

```
sudo apt install mariadb-server -y
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

Clone Repository
binaries erstellen
appsettings bearbeiten. IP, User und Passwort setzen
./WebAPI ausführbar machen (chmod 755)

## App starten

```
dotnet run --urls=http://0.0.0.0:44304/
```
