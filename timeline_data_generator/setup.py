"""Python setup file for timeline_data_generator package."""

from setuptools import setup, find_packages


setup(
    name='timeline_scraper',
    version='0.1.0',
    license='MIT',
    description='Generate data for timeline',
    long_description='Generate data for timeline',
    author='Avi',
    find_packages=find_packages('timeline_scraper'),
    package_dir={'': 'timeline_scraper'},
    install_requires=[]
)

