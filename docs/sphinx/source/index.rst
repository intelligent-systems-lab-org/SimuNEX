.. title:: Home

.. ifconfig:: RUNNING_IN_DOCKER

   .. include:: ../../README.md
      :parser: myst_parser.sphinx_

.. ifconfig:: not RUNNING_IN_DOCKER

   .. include:: ../../../README.md
      :parser: myst_parser.sphinx_

.. toctree::
   :hidden:
   :maxdepth: -1

   Home <self>

.. toctree::
   :hidden:
   :maxdepth: -1
   :caption: The Basics

   overview/get_started.rst