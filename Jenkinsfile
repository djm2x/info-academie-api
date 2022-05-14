#!groovy

node {
  def APP_NAME = 'infoacademie-api'
  def DOMAINE = 'infoacademie.com'
  def DOMAINE_PREFIX = ''
  def SUB_DOMAINE = 'api'
  def APP_PORT = '5000'
  def EXPOSED_PORT = '5000'

  def DOCKER_FILE_NAME = 'Dockerfile'

  def app

    stage('Cloning Git') {
      def commit = checkout scm
      //  env.BRANCH_NAME = commit.GIT_BRANCH.replace('origin/', '')
      // sh "echo ${commit}"
    }

    stage('Building image') {
      app = docker.build("${APP_NAME}", "-t ${APP_NAME} -f ${DOCKER_FILE_NAME} ./")

      // sh "echo ${app}"
    }

    stage('Docker Run') {
      sh "docker rm --force ${APP_NAME}"
      // sh "docker rmi --force ${APP_NAME}"
      // sh "docker run -d --name library-api -p 5000:5000  --env me=you 20a0acda5a32" 
      //&& PathPrefix(`/${DOMAINE_PREFIX}`)
      sh """docker run -d \
      --restart unless-stopped \
      --network proxy \
      --env "me=you" \
      -p ${EXPOSED_PORT}:${APP_PORT} \
      --label traefik.enable=true \
      --label traefik.http.routers.${APP_NAME}.tls=true \
      --label traefik.http.routers.${APP_NAME}.tls.certresolver=letsencrypt \
      --label traefik.http.routers.${APP_NAME}.rule='Host(`${SUB_DOMAINE}.${DOMAINE}`)'\
      --label traefik.http.services.${APP_NAME}.loadbalancer.server.port=${APP_PORT} \
      --name ${APP_NAME} \
      ${APP_NAME}"""

      // sh "docker system prune -a -f"
    }
}

