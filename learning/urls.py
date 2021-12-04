from django.conf.urls import url
from django.urls import include
from rest_framework.routers import DefaultRouter
from learning.views import TestViewSet

router = DefaultRouter()
router.register(r'tests', TestViewSet, basename='tests')

urlpatterns = [
    url(r'^', include(router.urls)),
]
