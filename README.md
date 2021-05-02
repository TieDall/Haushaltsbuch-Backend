# Getting Started

## Install Database and Tools

```
sudo apt install apache2 -y
sudo apt install php -y
cd /var/www/html
sudo rm index.html
sudo service apache2 restart
sudo apt install mariadb-server php-mysql -y
sudo service apache2 restart
sudo mysql_secure_installation

sudo nano /etc/mysql/my.cnf
```

Comment line:
```
#bind-address       = 127.0.0.1
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
sudo ln -s /usr/share/phpmyadmin /var/www/html/phpmyadmin

```

## Install .NET

```
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin
echo 'export DOTNET_ROOT=$HOME/.dotnet' >> ~/.bashrc
echo 'export PATH=$PATH:$HOME/.dotnet' >> ~/.bashrc
source ~/.bashrc
```
source: https://docs.microsoft.com/de-de/dotnet/iot/deployment

## Install App

## App starten

```
dotnet run --urls=http://0.0.0.0:44304/ --project [path to project]/Haushaltsbuch-Backend/WebApi/WebApi.csproj
```
