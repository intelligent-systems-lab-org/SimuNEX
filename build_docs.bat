@echo off
SETLOCAL EnableDelayedExpansion

REM Check for --host or -h argument
SET HOST_FLAG=0
FOR %%a IN (%*) DO (
    IF "%%a"=="--host" SET HOST_FLAG=1
    IF "%%a"=="-h" SET HOST_FLAG=1
)

REM Path to package.json, conf.py, and Doxyfile (adjust these paths as needed)
SET PACKAGE_JSON_PATH=.\package.json
SET CONF_PY_PATH=docs\sphinx\source\conf.py
SET DOXYFILE_PATH=docs\doxygen\Doxyfile

REM Extract version from package.json
FOR /F "tokens=2 delims=:, " %%i IN ('findstr "version" %PACKAGE_JSON_PATH%') DO (
    SET FULL_VERSION=%%i
    SET FULL_VERSION=!FULL_VERSION:"=!
)

REM Check if version was found
IF "!FULL_VERSION!" == "" (
    echo Error: Version not found in package.json
    exit /b 1
)

REM Extract major and minor version (assumes format major.minor.patch)
FOR /F "tokens=1,2 delims=." %%a IN ("!FULL_VERSION!") DO (
    SET VERSION=%%a.%%b
)

REM Update 'release' in conf.py
powershell -Command "(gc '%CONF_PY_PATH%') -replace '^release = .+', 'release = ''!FULL_VERSION!'' ' | Out-File -encoding ASCII '%CONF_PY_PATH%'"

REM Update 'version' in conf.py
powershell -Command "(gc '%CONF_PY_PATH%') -replace '^version = .+', 'version = ''!VERSION!'' ' | Out-File -encoding ASCII '%CONF_PY_PATH%'"

REM Update 'PROJECT_NUMBER' in Doxyfile
powershell -Command "(gc '%DOXYFILE_PATH%') -replace '^PROJECT_NUMBER\s+=.+', 'PROJECT_NUMBER         = !FULL_VERSION!' | Out-File -encoding ASCII '%DOXYFILE_PATH%'"

echo Updated Sphinx conf.py and Doxyfile with full version !FULL_VERSION! and version !VERSION!

REM Conditionally run the Docker container if --host or -h argument is provided
IF %HOST_FLAG% == 1 (
    
    REM Building the Docker image
    echo Building the Docker image for SimuNEX Web...
    docker build -t simunex-web:latest -f docs\Dockerfile . -q

    REM Check if Docker build was successful
    IF NOT %ERRORLEVEL% == 0 (
        echo Error: Docker build failed
        exit /b 1
    )

    echo Docker image built successfully.
    
    echo Running simunex-web Docker container...
    docker run --rm -it -p 80:80/tcp simunex-web:latest
) ELSE (
    echo Not running Docker container as no --host or -h argument was provided.
)

ENDLOCAL