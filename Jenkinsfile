pipeline {
    agent any

    environment {
        DOTNET_CLI_HOME = "C:\\Program Files\\dotnet\\6.0.416"
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build') {
            steps {
                script {
                    bat "dotnet restore"
                    bat "dotnet build --configuration Debug"
                }
            }
        }

        stage('Test') {
            steps {
                script {
                    bat "dotnet test \"Services.Tests.csproj\" --no-restore --no-build --verbosity normal --configuration Debug"
                }
            }
        }
    }
    
    post {
        success {
            echo 'Збірка та тестування завершені успішно!'
        }
    }
}
