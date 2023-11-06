# Configuration file for the Sphinx documentation builder.
#
# For the full list of built-in configuration values, see the documentation:
# https://www.sphinx-doc.org/en/master/usage/configuration.html

# -- Project information -----------------------------------------------------
# https://www.sphinx-doc.org/en/master/usage/configuration.html#project-information

import os
import json

project = 'SimuNEX'
copyright = '2023, Lee Bissessar'
author = 'Lee Bissessar'

# Path to the package.json file
package_json_path = os.path.join(os.path.dirname(__file__), '../..', 'package.json')

# Read and parse the package.json file
with open(package_json_path, 'r') as f:
    package_json = json.load(f)

# Extract the version number and assign it to 'release'
release = package_json.get('version', 'unknown')

# The short X.Y version
version = '.'.join(release.split('.')[:2]) 

# -- General configuration ---------------------------------------------------
# https://www.sphinx-doc.org/en/master/usage/configuration.html#general-configuration

extensions = ['breathe', 'myst_parser']
breathe_projects = { 'SimuNEX': './xml/output' }
breathe_default_project = 'SimuNEX'

templates_path = ['_templates']
exclude_patterns = ['_build', 'Thumbs.db', '.DS_Store']

# -- Options for HTML output -------------------------------------------------
# https://www.sphinx-doc.org/en/master/usage/configuration.html#options-for-html-output

html_theme = 'pydata_sphinx_theme'
html_static_path = ['_static']

html_title = 'SimuNEX Documentation'

html_sidebars = {"**": ["custom-toc-tree"]}

# Add Markdown file support
source_suffix = {
    '.rst': 'restructuredtext',
    '.md': 'markdown',
}

repo_name = 'SimuNEX'
