from django.conf.urls import url
from django.contrib import admin
from django.urls import path, include

from .yasg import urlpatterns as doc_urls

urlpatterns = [
    path('admin/', admin.site.urls),
    url(r'api/v1/', include('questions.urls')),
    url(r'^api/v1/auth/', include('djoser.urls')),
    url(r'^api/v1/auth/', include('djoser.urls.jwt')),
]

urlpatterns.extend(doc_urls)
