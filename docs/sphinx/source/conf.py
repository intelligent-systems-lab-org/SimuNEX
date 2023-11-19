# Configuration file for the Sphinx documentation builder.
#
# For the full list of built-in configuration values, see the documentation:
# https://www.sphinx-doc.org/en/master/usage/configuration.html

# -- Project information -----------------------------------------------------
# https://www.sphinx-doc.org/en/master/usage/configuration.html#project-information

import os

project = 'SimuNEX'
copyright = '2023, Lee Bissessar'
author = 'Lee Bissessar'

# Check if running in docker
running_in_docker = os.getenv('RUNNING_IN_DOCKER') == 'true'

# Extract the version number and assign it to 'release'
release = '0.5.3'

# The short X.Y version
version = '0.5'

# -- General configuration ---------------------------------------------------
# https://www.sphinx-doc.org/en/master/usage/configuration.html#general-configuration

extensions = ['myst_parser', 'sphinx.ext.ifconfig']
templates_path = ['_templates']
exclude_patterns = ['_build', 'Thumbs.db', '.DS_Store']

# -- Options for HTML output -------------------------------------------------
# https://www.sphinx-doc.org/en/master/usage/configuration.html#options-for-html-output

html_theme = 'pydata_sphinx_theme'
html_static_path = ['_static']

html_title = 'SimuNEX Documentation'

html_sidebars = {"**": ["custom-toc-tree"]}

html_favicon = "https://raw.githubusercontent.com/intelligent-systems-lab-org/simunex.github.io/main/img/favicon.png"

html_show_sourcelink = False

# Add Markdown file support
source_suffix = {
    '.rst': 'restructuredtext',
    '.md': 'markdown',
}

repo_name = 'SimuNEX'

def setup(app):
    app.add_config_value('RUNNING_IN_DOCKER', running_in_docker, 'env')
