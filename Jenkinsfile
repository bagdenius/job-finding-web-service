pipeline {
    agent any

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
    }
    
    post {
        success {
            echo 'Збірка завершена успішно!'
        }
    }
}
