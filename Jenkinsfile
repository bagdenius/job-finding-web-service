pipeline {
    agent any

    environment {
        DOTNET_CLI_HOME = 'C:\\Program Files\\dotnet\\6.0.416'
        AZURE_CLIENT_ID = credentials('jfws-app-id')
        AZURE_CLIENT_SECRET = credentials('jfws-secret')
        AZURE_TENANT_ID = credentials('jfws-tenant-id')
        AZURE_SUBSCRIPTION_ID = credentials('base-subscription-id')
        AZURE_RESOURCE_GROUP = credentials('jfws-resource-group')
        AZURE_WEB_APP_NAME = credentials('jfws-web-app-name')
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
                    bat 'dotnet restore'
                    bat 'dotnet build --configuration Debug'
                }
            }
        }

        stage('Test') {
            steps {
                script {
                    bat 'dotnet test \"Services.Tests\\Services.Tests.csproj\" --no-restore --no-build --verbosity normal --configuration Debug'
                }
            }
        }

        stage('Publish') {
            steps {
                script {
                    bat 'dotnet publish --no-restore --configuration Debug --output .\\publish'
                }
            }
        }

        stage('Deployment') {
            steps {
                script {
                    bat 'az account set --subscription %AZURE_SUBSCRIPTION_ID%'
                    bat 'az login --service-principal -u %AZURE_CLIENT_ID% -p %AZURE_CLIENT_SECRET% --tenant %AZURE_TENANT_ID%'
                    bat 'az webapp deploy --resource-group %AZURE_RESOURCE_GROUP% --name %AZURE_WEB_APP_NAME% --src-path .\\publish'
                }
            }
        }
    }

    post {
        success {
            echo 'Збірка, тестування, публікація та розгортання завершені успішно!'
        }
    }
}
